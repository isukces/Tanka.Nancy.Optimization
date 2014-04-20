namespace Tanka.Nancy.Optimization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public abstract class Bundle
    {
        private readonly List<string> _files;
        public static global::Nancy.IRootPathProvider rootPathProvider;

        protected Bundle(string path)
        {
            Path = path;
            _files = new List<string>();

            BundleTable.Bundles.Add(this);
        }

        public string Path { get; private set; }

        public IEnumerable<string> Files
        {
            get { return _files; }
        }

        public abstract string ContentType { get; }

        public void Include(string file)
        {
            _files.Add(file);
        }

        public string RenderHtml(bool optimize)
        {
            if (optimize)
                return RenderOptimizedHtml();

            return RenderUnoptimizedHtml();
        }

        protected abstract string RenderOptimizedHtml();
        protected abstract string RenderUnoptimizedHtml();

        private string GetFullPath(string path)
        {
            string rootPath = rootPathProvider.GetRootPath();
            return System.IO.Path.Combine(rootPath, path.TrimStart('/'));
        }

        public string GetCacheKey()
        {
            var builder = new StringBuilder();

            foreach (string file in Files)
            {
                var fullName = GetFullPath(file);
                var fileInfo = new FileInfo(fullName);
                builder.Append(fileInfo.LastWriteTimeUtc.ToFileTimeUtc());
                builder.Append(fullName);
            }

            string hashThis = builder.ToString();
            SHA1 hasher = SHA1.Create();
            byte[] hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(hashThis));

            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}