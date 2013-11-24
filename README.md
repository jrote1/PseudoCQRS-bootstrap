PseudoCQRS-bootstrap
====================

A bootstrap for PseudoCQRS. PseudoCQRS is a CQRS based framework to create web applications. Please visit https://github.com/LiquidThinking/PseudoCQRS to learn more about it.

 Create a new asp.net web project and install this as a package by typing the following in your package manager console.

Install-Package PseudoCQRS.Bootstrap

This will install everything which should need and plus some csharp code files will be copied into a new folder "Bootstrap"

Open your Global.asax.cs file and add this line

PseudoCQRS.Bootstrap.Bootstrapper.Initialize();

but before running your application, open the Bootstrapper.cs file which will be in Bootstrap folder and set the connection string.

