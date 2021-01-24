## Config Loader

This is a simple config tool for Autodesk Inventor to standardize Inventor options across users' computers. 

It works by editing a select subset of Inventor Application Options from a json configuration file. The thought is that the administrator can configure a json file and deploy it with this tool to the users.  They run the tool and their options are set to company standards.
You can find an example config file [here](https://github.com/jordanrobot/config-loader/blob/master/src/config.json).

### To json or not to json

A json file may not be the best method for configuration, but I was trying to avoid hard-coding the options into the main logic. These are some thoughts on using json files...

- Simple for anyone to configure, you don't need to edit the code
- You can make multiple json config files with different configs, perhaps I add a gui file selector into the routine? <== If you set up your code by passing in things like the file path into the related class and methods in that class well, it'll be cake to program it as a console app but then add a gui layer if you later decide that's the direction you want.
- where would these json files live?
    - Right next to the rule/application/add-in? 
    - Network location or locally?
    - in a library location that would be managed by the administrator?
    - Trying to predict needs is always going to fail, the answer to how people will use it is probably "All of these" so just set it up so the user can locate the file no matter what you do.
    - What you should do is store a second json file with the app that can be set up to have the location of the config json, that way the app can be deployed easily and still know where the json config is everyt ime
- How are the json files referenced in the code?
    - filenames are hardcoded? - no
    - user selectable? - yes
    - uses a single name, such as "config.json" - also yes
    - if a console application, uses a command line argument to specify the file - also yes. I would make it so if an arg is passed it overrides the above two.
- Because of how json escapes \ characters, you either have to use \\, or / when specifying paths. - Most json libraries take care of this without you knowing. Because of this, I would make it so the user doesn't edit the json unless they really know what they're doing. Use a /edit arg on your script that lets the user edit settings. Or just make it so they load everything into their inventor, change what they want, and save it off as a new json file.

### Thoughts on how this is used

That said, I'm not certain what form this tool will eventually take.  Right now it is a console application, but it could easily become an add-in or iLogic rule.  There are pros and cons to each, and these are also related to how the tool may actually be used; e.g. what is the actual workflow? - Start simple. Get 

#### iLogic Rule

- can be collated into a single rule for use on user's machines
- default config options can be set at the top of the rule, the json file could go away
- the admins can easily look through the code to determine what it is doing
- if located in an external directory, the users can run the rule themselves.
- The ilogic path would need to be added manually to run this rule for the first time.  This would slow down deployment.
- Perhaps a "bootstrap" inventor file could be used that points to the rule, or contains the entirety of the rule (not a fan of the latter).

#### Add-in

- would need to bundle
- could push to users' computers, or provide simple one-click installers
- could look at a central location for updates to the config.json file and would update automatically if the config changes
    - this wouldn't be too taxing, but is more stuff to load into inventor every time it starts up
- could be quickly run by users (if it was a button in the ribbon)
- config.json could be bundled with the add-in.  Updates would require an updated installer of the add-in.

#### Program

- Admins may be hesitant to run since they would have to inspect code and build themselves to be sure of security. < - Aren't you going to release the source code? That lets the extra paranoid ones inspect and build it themselves. Add-in also has this consequence.
- Can execute on users' machines without their involvement (can deploy easily)
- can use input arguments to specify behavior or config files
