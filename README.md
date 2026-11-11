# CG Tutorial work (creating a mask shader)
\
Created by: Charlie Burnham\
\
This shader made during my computer graphics tutorial uses mask shaders to simulate blood or damage on an enemy. As you can see in the image below a basic bloodstain texture was made to be used for this shader. It was made in Krita.\
<img width="1024" height="1024" alt="Bloodstain White" src="https://github.com/user-attachments/assets/edd2bf50-173c-4969-941a-ebd23cf09ff2" />\
\
The shader itself is structured as seen below\
<img width="1039" height="524" alt="Screenshot 2026-11-11 132843" src="https://github.com/user-attachments/assets/b207f89d-f7d9-4989-88cc-91bb62501d28" />\
\
Once the shader was applied to the material the values were set up as seen below.\
<img width="532" height="393" alt="Screenshot 2026-11-11 133000" src="https://github.com/user-attachments/assets/7d394497-fd01-4b5c-a0bd-608abd421d29" />\
\
Then in the enemycode a material is defined at the start function and upon the enemy taking damage the float value controlling the mask is set to 1.\
<img width="503" height="28" alt="Screenshot 2026-11-11 133123" src="https://github.com/user-attachments/assets/860e16ba-0ff3-4ccb-9ec0-18386b0f0ac5" />\
<img width="327" height="24" alt="Screenshot 2026-11-11 133133" src="https://github.com/user-attachments/assets/3b62964f-1b24-476c-b1c6-f2d616ebb20c" />\
\
The end result is that upon being hit the enemy shows a blood texture on them. The before hit and after hit visuals are seen below.\
<img width="222" height="183" alt="Screenshot 2026-11-11 133330" src="https://github.com/user-attachments/assets/0c4dde69-af79-4606-a049-79adf5d128ec" />\
<img width="133" height="160" alt="Screenshot 2026-11-11 133346" src="https://github.com/user-attachments/assets/61ba209d-3331-4cff-9e1c-c84e87840ba0" />\
