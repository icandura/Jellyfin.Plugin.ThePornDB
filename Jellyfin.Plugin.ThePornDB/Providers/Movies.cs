using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities.Movies;
using MediaBrowser.Controller.Providers;
using MediaBrowser.Model.Providers;
using ThePornDB.Helpers;

#if __EMBY__
using MediaBrowser.Common.Net;
#else
using System.Net.Http;
#endif

namespace ThePornDB.Providers
{
    public class Movies : IRemoteMetadataProvider<Movie, MovieInfo>
    {
        public string Name => Plugin.Instance.Name + " Movies";

        public async Task<IEnumerable<RemoteSearchResult>> GetSearchResults(MovieInfo searchInfo, CancellationToken cancellationToken)
        {
            var result = await Base.GetSearchResults(searchInfo, SceneType.Movie, cancellationToken).ConfigureAwait(false);

            return result;
        }

        public async Task<MetadataResult<Movie>> GetMetadata(MovieInfo info, CancellationToken cancellationToken)
        {
            var result = await Base.GetMetadata(info, SceneType.Movie, cancellationToken).ConfigureAwait(false);

            return result;
        }

#if __EMBY__
        public Task<HttpResponseInfo> GetImageResponse(string url, CancellationToken cancellationToken)
#else
        public Task<HttpResponseMessage> GetImageResponse(string url, CancellationToken cancellationToken)
#endif
        {
            return UGetImageResponse.SendAsync(url, cancellationToken);
        }
    }
}
