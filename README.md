This project contains code to track changes in the target file and this happens at every 15 seconds.

Color Combinations used
  1. Green -> Added
  2. Red -> Deleted
  3. [Red Green] -> A Red character followed by Green characted inside a square bracket indicates that the character has been updated. Red indicates previous character and Green indicated new.

Working 

![image](https://github.com/chakrei/coremaxtech_assessment/assets/8541154/bf4a5c6d-d662-4b7e-bf3b-bafc50ec1cc5)

Please enter the path of file you want to track. Only "txt" files are allowed for now. If more file types are needed, this can be modified in "App.Config" file.

Below is the initial step once you enter a valid file.
![image](https://github.com/chakrei/coremaxtech_assessment/assets/8541154/32f6359e-943e-470b-bb68-08c44e97adfb)
As changes are tracked for the first time, all data is considered to be added.

For every 15 seconds, program will check if the tracking file has been updated with new contents and if there is no change, it displays the last modified date as shown below
![image](https://github.com/chakrei/coremaxtech_assessment/assets/8541154/99b2ec4c-d4fc-4c4b-806a-b96869abbc58)

Once, there is a change in the contents, we are representing as below
![image](https://github.com/chakrei/coremaxtech_assessment/assets/8541154/3968092d-0c2f-4027-8ca7-061225684cb7)


Things to keep in mind,

This is not a fulfledged implementation of how a Google doc or Git file history works. This is only a sample to track what has changed at a very basic level 
Thi
