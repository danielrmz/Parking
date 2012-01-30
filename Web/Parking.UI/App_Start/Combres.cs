[assembly: WebActivator.PreApplicationStartMethod(typeof(Parking.UI.App_Start.Combres), "PreStart")]
namespace Parking.UI.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}