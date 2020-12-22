const initVideo = () => {
    //setup video
    const video = new DocTogetherVideo('.video-panel', {
        onHangUp: () => {
            //this allows you to define what happens when you hang up
            document.querySelector('.video-panel').classList.add('hide');
            document.querySelector('.join-meeting-panel').classList.remove('hide');
        },
        onConnect: () => {
            //this allows you to define what happens when you connect
            document.querySelector('.video-panel').classList.remove('hide');
            document.querySelector('.join-meeting-panel').classList.add('hide');
        }
    });
    video.init();

    //attach the onclick event to join the meeting
    document.querySelector('#joinMeetingBtn').onclick = () => {

        //get the name and meeting to use.
        let name = document.querySelector('#name').value;
        let meeting = document.querySelector('#meeting').value;

        //for our demo if they don't have a name require it
        if (!name || name.length === 0) {
            alert('Your name is required');
            document.querySelector('#name').focus();
            return;
        }

        //call our server to get the meeting room info so we can connect


        fetch(`../Home/RoomInfo/${meeting}`)
            .then(response => {
                response.json().then(data => {
                    data.userName = name;
                    let server = document.querySelector('#Server');
                    if (server && server.value != "0") {
                        data.socketToken += server.value;
                        data.videoServerUrl = `https://${server.value}/janus`;
                    }
                    if (confirm(data.videoServerUrl)) {
                        console.log(data);
                        video.connect(data);
                    }
                });
            });
    };
};


window.onload = initVideo;