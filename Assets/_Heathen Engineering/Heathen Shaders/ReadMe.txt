Heathen's Selective Glow system in comprised of 2 major parts
1) The immage effect shader "Selective Glow" which is availabel under "Components >> Heathen >> Image Effects >> Selective Glow"
2) Material shaders (various) which can be found in your material shaders sorted under Heathen >> Glow

Once you have a material and the immage effect in place you're all done; note the glow effect will not show in the editor as its an OnRender effect
you can however see it in your Game view.

Tip: Glow Maps dont have to be white while this does allow them to pick up your Glwo Color very nicely you can get multi-color glows by using a colored
Glow Map similar to what was done with the Heathen Engineering's Selective Glow title image used on the _Demo

Tip: You can control intensity and quality from several points of view each has its advantages and disadvantages depending on your particular scene be sure
to play with the Inner and Outter as well as Glow Color Alpha on your materials as well as the global Intensity on the Immage Effect Selective Glow shader



Credits (for the _Demo assets)
Triplanar Object Bump shader and derived triplanar shadars are dirivitive works from the excelent work of ToxicFork's Marching Cubes 
(https://bitbucket.org/toxicFork/marching-cubes/overview)
While the code is small fragment of the original work and is changed it is derived and bestpractice is to include this should you re-use/distribute the shader
Note his open license agreement with regards to the source of his works.

"
THIS PROJECT IS INCOMPLETE

Copyright (c) 2012+ Firtina 'toxicFork' | 'Hergonan' Ozbalikci

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
"

As for the metal textures
The Metal textures used in the _Demo objects included in this asset are from Nobiax Yughues (http://nobiax.deviantart.com/) who offers many of his textures
free of charge right on the Unity store (https://www.assetstore.unity3d.com/#/publisher/4986)