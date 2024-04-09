"use strict"

let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

const messageContainer = document.getElementById('message-container');
const sendBtn = document.getElementById('sendBtn');

const messageInput = document.getElementById('msg-content');
const receiverInput = document.getElementById('receiver-input');
const senderInput = document.getElementById('sender-input');

const receiverMessageStyle = 'card px-4 rounded-pill py-2 align-self-start text-white bg-primary';
const senderMessageStyle = 'card px-4 rounded-pill py-2 align-self-end text-primary bg-white border-primary';

connection.start().catch(e => {
    console.log(e.message);
})

connection.on("ReceiveMessage", function (messageObj) {
    let div = document.createElement('div');

    div.className = messageObj.isSender ? senderMessageStyle : receiverMessageStyle;
    div.textContent = messageObj.content;

    messageContainer.appendChild(div);
})

sendBtn.addEventListener('click', (e) => {
    e.preventDefault();

    let messageObj = {
        Content: messageInput.value,
        ReceiverId: receiverInput.value,
        SenderId: senderInput.value,
    }

    messageInput.value = '';

    connection.invoke("SendMessage", messageObj).catch((e => {
        console.log(e.message);
    }))
})
