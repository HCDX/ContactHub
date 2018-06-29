# ContactHub
ContactHub is a web application developed with C#, MS SQL Server, Javascript, Bootstrap.
The application is an address book storing contacts of all partners and humanitarian in the operation (Lebanon).
The contacts are then organized based on their sectors (Protection, Shelter, SGBV, etc.) and their areas of interest (National, North, South, Bekka, etc.).
Each focal point (for a Sector and area of interest) in the ContactHub manages is own list of contacts and can share them with other focal point.
The application has functionalities to search for a contact, print list of contact in a specific sector and area of interest.

Please follow the below to customize the application
1. Go to line 8 on immain.Master and change the tracking. You can generate your own tracking javasript code from https://analytics.google.com

2. Go to line 36 App_Code/clsEmail.cs. Add your email address and its name from which the email will be sent. In the same page, go to line 43 and enter your network username and password. In my case, I used SendGrid which is a good system. Check SendGrid price and offers from https://sendgrid.com/. You can also access SendGrid plattform using your Microsoft Azure account.

3. Go to lines 647 and 682 on Subscription.aspx.cs, add your email address and its name from which the email will be sent to subscriber or users. After that go to the lines 342 and 389 to add your web folder URL (e.g http://myserver/myfolder/)


4. Go to line 1095 on ContactList.aspx.cs, add your email address and its name from which the email will be sent to subscriber or users.

After these configuration, your need to setup your database in MS SQL Server and also you need to create your library (DLL).

For any questions or suggestions aiming to improve this application. Please feel free to send a message.
