import * as signalR from '@microsoft/signalr'

const connection = new signalR.HubConnectionBuilder()
  .withUrl('http://localhost:8082/Hubs/FileUploadStatusHub', {})
  .build()

connection.on('RecieveMessage', data => {
  let [user, message] = data
  console.log(user, message)
})
