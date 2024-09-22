using System;
using DryIoc;
using Foundation;
using UIKit;

namespace InvalidProgram;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public override UIWindow? Window { get; set; }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        Window = new UIWindow { RootViewController = new UIViewController { View = { BackgroundColor = UIColor.Green } } };
        Window.MakeKeyAndVisible();

        var container = new DryIoc.Container(rules =>
            rules
                .WithFuncAndLazyWithoutRegistration()
                .WithUseInterpretation());
        
        // register an IThing and two services which depend on Lazy<Thing>
        container.Register<Thing>(Reuse.Singleton);
        container.Register<ServiceA>(Reuse.Singleton);
        container.Register<ServiceB>(Reuse.Singleton);
        
        // resolve a service that depends on Lazy<Thing> once - works
        Console.WriteLine("Resolve a service that depends on Lazy<Thing> once");
        container.Resolve<ServiceA>();
        Console.WriteLine("Ok");
        
        // resolve the same service that depends on Lazy<Thing> a second time - works
        Console.WriteLine("Resolve the same service that depends on Lazy<Thing> a second time");
        container.Resolve<ServiceA>();
        Console.WriteLine("Ok");
        
        // resolve a different service that depends on Lazy<Thing> - crashes
        Console.WriteLine("Resolve a different service that depends on Lazy<Thing>");
        container.Resolve<ServiceB>();
        Console.WriteLine("Ok"); // we don't make it here
        
        return true;
    }
}

public class Thing;
public class ServiceA(Lazy<Thing> lazyThing);
public class ServiceB(Lazy<Thing> lazyThing);
