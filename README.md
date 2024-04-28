<!--    -->
<h1 align="center"> 
Yunasawa の Library <br></br>
Runtime Debug Command - Documentation 
</h1>

<h4 align="center"> <i>Documentation</i> shows you how to use <i>RUNTIME DEBUG COMMAND</i>.</h4>

<p align="center">
 <img src="https://img.shields.io/badge/Script-DOCUMENTATION-blue.svg" alt="script">
 <img src="https://img.shields.io/badge/Debug%20Command-008F64">
 <img src="https://img.shields.io/badge/Contact-yunasawa200@gmail.com-purple.svg" alt="email">
</p>

<h2> ★ About </h2>

- <b><i>Runtime Debug Command</i></b> provides you an input field used to handle and debug your logics, events or you can use it as a feature of you game. 
- See <a href="https://github.com/Yunasawa/Realtime-Debug-Command/blob/main/VERSION.md"><b>Version</b></a> for more updated features.
- This tool is not really perfect and complete so if you have any errors or bugs or difficulties when use this, please feedback and I will response as soon as possible.

<h2> ★ Table On Contents </h2>

- <a href="#how-to-create-debug-command-gui"> ★ How to create Debug Command GUI </a><br>
- <a href="#how-to-create-new-debug-command"> ★ How to create new Debug Command </a><br>
- <a href="#how-to-use-debug-command-setting"> ★ How to use Debug Command Setting </a><br>

<h2><div id="how-to-create-debug-command-gui"> ★ How to create Debug Command GUI </div></h2>

<h3><i> From Menu Item / Hierarchy (Currently not supported)</i></h3>

- First, you can select an object in hierarchy to be parent of RDC.
- Right-click in hierarchy, select <kbd>YのL</kbd> > <kbd>2D</kbd> > <kbd>Runtime Debug Command</kbd> or select <kbd>Tools</kbd> > <kbd>YのL</kbd> > <kbd>Create Object</kbd> > <kbd>2D</kbd> > <kbd>Runtime Debug Command</kbd> on window bar.
- In case you don't select a canvas to be the parent, a new Canvas and Event System will be created automatically.

<h3><i> From Prefab / Asset </i></h3>

- Find RDC in <kbd>Assets</kbd> > <kbd>Yunasawa の Library</kbd> > <kbd>Runtime Debug Command</kbd> > <kbd>Prefabs</kbd> > <kbd>Debug Command GUI</kbd>.
- Drag it into an object that you want it to be the parent of RDC.

<h2><div id="how-to-create-new-debug-command"> ★ How to create new Debug Command </div></h2>

<ul>
<li> Here is a sample code for a <img width="12%" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/829b30b5-10ac-4ade-8d2b-1dab8ce6af3a">, I call it DC_Debug. It is used to display a message inside log window with a general command of <img align="center" width="22.5%" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/1288567b-6599-4c4c-8be6-65fc37375adb">. </li>

<br><img align="center" width="95%" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/51174105-4fd7-44b7-95b8-bd02a1c53e89"></br>

<li> As you can see on the sample picture, now I will show you how to make one step by step: </li>
   <ul>
   <li> First, create a new class (name it whatever you want, I recommend to put DC_ in the beginning), inherited from <img width="12%" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/829b30b5-10ac-4ade-8d2b-1dab8ce6af3a">. </li>
   <li> Make a constructor for it, now you have to concentrate on this step. <b>CommandNodes</b> is a <b>List</b> of <b>CommandNode</b>. Here is <b>CommandNode</b> class: </li>
<img align="center" width="90%" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/ff7f6d97-4a9a-4c6d-a003-9bbbfb9911d1"><br>
   As you can see, <b>CommandNode</b> has 3 properties, Nodes, Suggestions, MustStartWith and Customable. 

   - <b>Nodes</b> is the general name of node in a command. For example, in the command <img align="center" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/1288567b-6599-4c4c-8be6-65fc37375adb">, <b>Nodes</b> are <i>"debug", "selection", "message"</i>.
   - <b>Suggestions</b> will show up when you typing the commands so you can <kbd>Tab</kbd> to finish it automatically, when you typing the <i>"selection"</i> node of <img align="center" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/1288567b-6599-4c4c-8be6-65fc37375adb">, a list of <img align="center" height="17.5" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/d8f93816-51b1-4f4a-ba94-164b0ec15fcd"> will show up.
   - <b>MustStartWith</b>, if you enable this only the suggestions which start with the word you're typing will appear. You are in <i>"selection"</i> node, then you type "n" then only "notify" appears, but if it's disabled, "warning", "caution" and "notify" will show up (Those 3 contain "n").
   - <b>Customable</b> allows you to set that node is customable or not, it means you can type anything in that node and no need to follow the suggestions.
   <li> Back to sample DebugCommand, you can see inside the constructor, I assign <b>CommandNodes</b> with a new list, inside I make new <b>CommandNode</b> objects with inputing params are <b>Nodes, Suggestions, MustStartWith, Customable</b>.</li>

   - Node 0: <img align="center" height="17.5" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/73f9921b-fa12-4fcf-9a41-9f80aad73740">, it have <i>"debug"</i> as <b>Nodes</b>, <i>"debug"</i> as <b>Suggestions</b> and <b>MustStartWith</b> is <i>true</i>.
   - Node 1: <img align="center" height="17.5" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/943a8252-299f-4190-900a-e6d870ff2397"> is similar.
   - Node 2: <img align="center" height="17.5" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/8890b2aa-673c-4f56-8c4b-d3082feb3ce7">, because I don't need <b>Suggestions</b> for this so I don't put anything inside, <b>StartWith</b> is defaulted by <i>false</i> and this node is <b>Customable</b>, you can type anything in this node.

  <li> After finish the constructor, call an override void named <img align="center" height="17.5" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/6fde6893-8b8d-4c79-87a2-3dc6bae6c448">, <b>value</b> is an array of words separated by <i>space</i>. For example, in command <img align="center" height="17.5" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/77c7f55e-a044-45ad-bfbe-aeb4b7eb2e8a">, <b>value</b> is <i>{ "debug", "log", "Hello", "world" }</i>. Inside this method, you can do your own code to handle the command just like above sample code.</li>

  <li> You can use <img align="center" height="17.5" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/8013f74b-68f8-4ef6-966a-7ee49df5d7e1"> to automatically check for wrong command and block the executer.</li>
  <li> Use <img align="center" height="17.5" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/30cfca86-22c8-4a4a-8514-fe3ae66266b2"> to show your command's result on Command Log as a message.
 </ul>
 <br>
 
 <li> Here is another sample for <img width="12%" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/829b30b5-10ac-4ade-8d2b-1dab8ce6af3a"> called DC_Time, used to manage time in game. </li>
<br><div align="center"><img width="90%" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/1b3611c8-298e-41a1-8ae9-b6b184f07abd"></div><br>

 <li> After you created a new <img width="12%" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/829b30b5-10ac-4ade-8d2b-1dab8ce6af3a">, go inside <kbd>DebugCommandObject.cs</kbd> and there're 2 things you have to notice: </li><br>
 <table>
   <tr> 
     <td>
      <ul>
      <li>Find this <b>CreateCommand()</b> method in <b>DebugCommandList</b> class.</li>
      <li>Inside this <b>switch</b>, add the first node of commands as <b>case</b> and return the <b>command</b> like this.</li>
      </ul>
     </td>
     <td><div align="right"><img width="100%" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/2c34de3c-b1c1-4460-b5e4-72c9e026d8c8"></div></td>
   </tr>
   <tr> 
     <td>
      <ul>
      <li>Then find this <b>CommandList</b> inside <b>DebugCommandObject</b> class.</li>
      <li>Add the first node of the command inside the list of string like this.</li></li>
      </ul>
     </td>
     <td><div align="right"><img width="100%" src="https://github.com/Yunasawa/Runtime-Debug-Command/assets/113672166/fd4704a8-a119-4dfb-800f-bec2c2f3ea8e"></div></td>
   </tr>
 </table>
 <li> Finish those thing and you now can use your own <img width="12%" src="https://github.com/Yunasawa/Realtime-Debug-Command/assets/113672166/829b30b5-10ac-4ade-8d2b-1dab8ce6af3a"> right inside your project. </li>
</ul>

<h2><div id="how-to-use-debug-command-setting"> ★ How to use Debug Command Setting </div></h2>

You can select <b>Debug Command GUI</b> object to make your custom setting for RDC.

