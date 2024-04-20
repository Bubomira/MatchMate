"use strict"

let connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

const messageContainer = document.getElementById('message-container');
const sendBtn = document.getElementById('sendBtn');

const messageInput = document.getElementById('msg-content');
const receiverInput = document.getElementById('receiver-input');
const senderInput = document.getElementById('sender-input');

const receiverMessageStyle = 'card px-4 rounded-pill py-2 align-self-start text-white bg-primary';
const senderMessageStyle = 'card px-4 rounded-pill py-2 align-self-end text-primary bg-white border-primary';

let scrollCount = 0;

connection.start().then(() => {
    console.log("Here!")
    messageContainer.scrollTop = messageContainer.scrollHeight;
}).catch(e => {
    console.log(e.message);
})

connection.on("ReceiveMessage", function (messageObj) {

    if (messageObj.receiverId == receiverInput.value && messageObj.senderId == senderInput.value) {
        let div = document.createElement('div');

        div.className = messageObj.isSender ? senderMessageStyle : receiverMessageStyle;
        div.textContent = messageObj.content;

        messageContainer.appendChild(div);
        messageContainer.scrollTop = messageContainer.scrollHeight;
    }
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

messageContainer.addEventListener('scroll', () => {
    if (messageContainer.scrollTop == 0) {
        fetch('https://localhost:7000/Matcher/Message/GetPreviousMessages', {
            method: 'post',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                senderId: senderInput.value,
                receiverId: receiverInput.value,
                scrollerCount: scrollCount
            })
        }).then(res => res.json()).then(messages => {
            messages.forEach(message => {
                var div = document.createElement('div');

                div.className = message.senderId == senderInput.value ? senderMessageStyle : receiverMessageStyle;
                div.textContent = message.content;

                messageContainer.prepend(div);
            })

            scrollCount++;

        })
            .catch(e => console.log(e));
    }
})