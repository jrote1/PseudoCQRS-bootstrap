PseudoCQRS-bootstrap
====================

This is a bootstrapper for PseudoCQRS framework. If you don't know about PseudoCQRS then please visit https://github.com/LiquidThinking/PseudoCQRS to learn more about it.

In order to use this, first create a asp.net web project and then in Package Manager Console type:

Install-Package PseudoCQRS.Bootstrap

This will install Bootstrap project including some source files which will be copied in your Project in "Bootstrap" folder.

Then open your Global.asax.cs file and add this line

PseudoCQRS.Bootstrap.Bootstrapper.Initialize();

but before running your application, open the Bootstrapper.cs file which will be in Bootstrap folder and set the connection string.

