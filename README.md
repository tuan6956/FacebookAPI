# FacebookAPI
Release 21/01 - Download : https://goo.gl/hiUtkx

//----------------------------------Developed by Tuan6956----------------------------------//

bool - Follow (string UID)

bool - AddFriend (string UID)

bool - Comment (string ID_Post, string Message)

bool - Like (string ID_Post)

bool - UpdateStatus (string Status,out string IdPost) - UpdateStatus(string Status, string Link, out string IdPost)

bool - UploadPhoto (string Status, string UrlPicture, out string IdPost)

bool - Share (string IDShare,string IdPost)

string[] GetFriendRequest()

bool - CheckLive()


-----------------------------------------------------------------


Example:

using FacebookAPI;

...
...

FbApi fb = new FbApi("token");

bool IsLive = fb.CheckLive();
