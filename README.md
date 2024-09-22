## what
Repro of regression? somewhere between net9p4 and net9rc1 when using DryIoC with its own interpreter on iOS.

Consider dependency graph:
* `ServiceA` --> `Lazy<Thing>`
* `ServiceB` --> `Lazy<Thing>`

with `ServiceA`, `ServiceB`, `Thing` registered as singletons.

Resolving either `ServiceA` or `ServiceB` repeatedly will succeed.

Resolving `ServiceB` after `ServiceA`, or `ServiceA` after `ServiceB`, will fail with `InvalidProgramException`.

Does not fail if the dependency is on `Thing`, rather than `Lazy<Thing>`.
Does not fail on net9p4.

## how
Be running net9rc1 with `ios` workload, have a device plugged in, then

`./net9.sh {Your Physical Device UDID}`

_Terminating app due to uncaught exception 'System.InvalidProgramException', reason: ' (System.InvalidProgramException)
at DryIoc.Interpreter.TryInterpretSingletonAndUnwrapContainerException(IResolverContext r, Expression expr, ImMapEntry`1 itemRef, Object& result)
at DryIoc.Factory.ApplyReuse(Expression serviceExpr, Request request)
at DryIoc.Factory.GetExpressionOrDefault(Request request)
at DryIoc.Container.ResolveAndCache(Int32 serviceTypeHash, Type serviceType, IfUnresolved ifUnresolved)
at DryIoc.Container.DryIoc.IResolver.Resolve(Type serviceType, IfUnresolved ifUnresolved)
at DryIoc.Resolver.Resolve[IServiceB](IResolver resolver, IfUnresolved ifUnresolved)
at InvalidProgram.AppDelegate.FinishedLaunching(UIApplication application, NSDictionary launchOptions)
at InvalidProgram.AppDelegate.__Registrar_Callbacks__.callback_2_InvalidProgram_AppDelegate_FinishedLaunching(IntPtr pobj, IntPtr sel, IntPtr p0, IntPtr p1, IntPtr* exception_gchandle)
'_