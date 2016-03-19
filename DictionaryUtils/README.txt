In order to use the HunspellWordDefiner class to define words, you need 2 things:

1. the hunspell runtime DLLs have to be available at runtime, and
2. the hunspell .dic and .aff files for your language of choice have to be available at runtime.

By "available at runtime" I mean that those files have to be located wherever your
application is running from at the time that you try to use one of the
APIs in HunspellWordDefiner.  

For example, if you're debugging a web app that's going to use HunspellWordDefiner, then the DLLs
and language files have to be located in your iis-express folder, and/or whatever location you're
debugging process is executing from.

The language filenames are currently hardcoded in HunspellWordDefiner.cs to en_us, so changing
those and managing deploy-time assets is a whole 'nother issue.
