# KoolTweets

## User guide:
KoolTweets is an application that retrieves tweets from API https://badapi.iqvia.io/swagger/ .
The app can retrieve tweets for a large time frame. 
It takes 2 inputs, start date and end date. 
 
Select the start date and end date from the date picker. Ensure that start date is earlier than the end date and all dates are in the past. Click on Get Tweets button to get the data. 
The date picker has 2 buttons. The one on the left gives a calendar and on the right is the time picker.
 
The date picker has left and right buttons to navigate between months and years. Clicking on the text between the arrows allows to zoom the calendar back
The time picker also has Top and Bottom arrows to set the time. 

## Output:
The tweets are displayed in a table with headers ID, Time Stamp and Message. The count of the total number of records retrieved is shown on the top left. There is a provision to download the data as a CSV file. Clicking the Download button on the right allows you to do this.


## Downloading and Running the App
Download or clone KoolTweets application from https://github.com/mohansuraj165/KoolTweets.
Set Project KoolTweets as the startup app and click the run button  

## Unit Tests
The KoolTweetsTest project contains unit tests. 
To open the Test Explorer go to Test->Windows->Test Explorer. Click Run All to run the tests.
