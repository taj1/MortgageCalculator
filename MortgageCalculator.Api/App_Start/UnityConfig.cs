using System;
using System.Linq;
using Unity;
using Unity.RegistrationByConvention;

namespace MortgageCalculator.Api
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              //Get the list of dependent assemblies
              var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(t => t.FullName.StartsWith("MortgageCalculator"));

              var container = new UnityContainer();
              container.RegisterTypes(
                 AllClasses.FromAssemblies(assemblies),
                 WithMappings.FromMatchingInterface,
                 WithName.Default);

              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

    }
}