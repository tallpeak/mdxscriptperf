# MDX Script Performance Analyzer
#### Copied from Chris Webb's BI Blog at: 
##### https://blog.crossjoin.co.uk/2007/04/20/mdx-script-performance-analyser
##### http://mdxscriptperf.codeplex.com/

#### Modified by Aaron West 
* Added minOf # for more stable timings: try 3 or 4
* Widened text boxes
* Removed maximization of window

> I liked the concept, but thought it needs some improvements due to the timings jumping around, leading to spurious spikes (many negative timings when adding a calculation)

> Saving to github to remind me to work on this a bit when I have more time

* TODO:
* Bump priority of worker thread
* Remove exception handling from MDXScript parser
* Cache-warming to stabilize timings a bit?
* Verify Nuget package restore of Microsoft.AnalysisServices.AdomdClient.dll?
* Remove dependency on Microsoft.ExceptionMessageBox.dll