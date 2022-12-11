const cookieObj = new URLSearchParams(document.cookie.replaceAll("&", "%26").replaceAll("; ", "&"))
var color = cookieObj.get("color")
console.log(color)

let obj = JSON.parse(color);

document.body.style.backgroundColor = obj.Name