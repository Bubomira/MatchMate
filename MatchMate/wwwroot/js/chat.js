"use strict"

let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

const messageContainer = document.getElementById('message-container');
const sendBtn = document.getElementById('sendBtn');

connection.start().catch(e => {
    console.log(e.message);
})

connection.on("ReceiveMessage", function (messageObj) {
    let div = document.createElement('div');
    div.className += ' card px-4 rounded-pill py-2 ';
    div.className += messageObj.isSender ? 'align-self-end text-primary bg-white border-primary' : 'align-self-start text-white bg-primary';
    div.textContent = messageObj.content;

    messageContainer.appendChild(div);
})

sendBtn.addEventListener('click', (e) => {
    e.preventDefault();

    let messageObj = {
        Content: document.getElementById('msg-content').value,
        ReceiverId: document.getElementById('receiver-input').value,
        SenderId: document.getElementById('sender-input').value,
    }

    document.getElementById('msg-content').value = '';

    connection.invoke("SendMessage", messageObj).catch((e => {
        console.log(e.message);
    }))
})
