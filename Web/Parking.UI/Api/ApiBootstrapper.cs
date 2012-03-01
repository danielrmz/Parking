/**
 *
 * @package     Parking.UI
 * @author      The JSONs
 * @copyright   2012 - 20XX
 * @license     Propietary
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;
using Nancy.Bootstrapper;

using Sieena.Parking.API.Modules;
using Sieena.Parking.API.Modules.Classes;

namespace Parking.UI.Api
{
    public class ApiBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoC.TinyIoCContainer container, IPipelines pipelines)
        {
            BeforePipeline bp = new BeforePipeline();

            // Since we are including the API in the Parking.UI package (because of server constraints)
            // we need to remove the prefix folder so the routes are taken correctly.
            bp.AddItemToStartOfPipeline(ctx => {
                ctx.Request.Url.Path = ctx.Request.Url.Path.Replace("/api", "");
                return ctx.Response;
            });

            pipelines.BeforeRequest += bp;

            // Register gzip compression
            pipelines.RegisterCompressionCheck();

            base.ApplicationStartup(container, pipelines);
        }
    }
}