# Duel of the Agents Console Application. 

This .NET C# console application hosts a debate between a boisterous pirate and a serene Buddhist Monk. Two agents use OpenAI's ChatGPT 4o model. 

## 1. Instructions

Set your OpenAI API key as an environment variable. 
### Windows 
Setting the environment variable 
```
set openai_apikey="your_openai_apikey"
```
Verify
```
echo %your_openai_apikey%
```
### MacOS / Linux
Use the following command to set a **temporary** environment variable for the current Terminal session. 
```
export openai_apikey="your_openai_apikey"
```

To set a **permenant** system wide environment variable, see this post 

https://stackoverflow.com/questions/135688/setting-environment-variables-on-os-x

Verify 
```
echo $openai_apikey
```

## 2. To Run
Change your working directory to DuelOfAgentsConsole 
```
dotnet build
dotnet run
```

Enter the debate topic and desired number of rounds. 

Enjoy! Arrr!


## 3. One more thing
I didn't have the time to build a frontend website for this. Here is something I coded many years ago using Angular2 and Typescript. 

https://printfcrlf.github.io/

and the source code 

https://github.com/printfCRLF/crlf/tree/master/web/FIS/fis-app



