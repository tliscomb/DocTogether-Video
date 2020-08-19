class DocTogetherVideo {
    constructor(uiSelector, options) {
        this.uiSelector = uiSelector;
        this.initComplete = false;
        this.options = options || {};
        this.url = this.options.url || 'https://conference.doctogether.com';
    }

    init() {
        if (this.initComplete) return;

        let el = this.el = document.querySelector(this.uiSelector);
        if (!this.el) {
            console.log(`Could not find element ${this.uiSelector}`);
            return;
        }
        
        let iframe = `<iframe src='${this.url}/video-conference' style='width:100%;height:100%;border:none;' allow='camera; microphone; autoplay; fullscreen'></iframe>`;
        this.el.innerHTML = (iframe);

        this.iframe = el.children[0];
        this.videoWindow = this.iframe.contentWindow;

        let processMsg = (x) => {
            let { data } = x
            console.log('parent message from react', data)
            switch (data.type) {
                case 'HANG_UP':
                    console.log('parent window hanging up')
                    this.hangUp();
                    break;
                case 'MUTE_AUDIO':
                    console.log('type:', data.type, 'payload', data.payload);
                    break;
                case 'UNMUTE_AUDIO':
                    console.log('type:', data.type, 'payload', data.payload);
                    break;
                case 'MUTE_VIDEO':
                    console.log('type:', data.type, 'payload', data.payload);
                    break;
                case 'UNMUTE_VIDEO':
                    console.log('type:', data.type, 'payload', data.payload);
                    break;
                case 'SHARE_SCREEN':
                    console.log('type:', data.type, 'payload', data.payload);
                    break;
                case 'STOP_SHARE_SCREEN':
                    console.log('type:', data.type, 'payload', data.payload);
                    break;
                case 'MESSAGE':
                    console.log('type:', data.type, 'payload', data.payload);
                    break;
            }
        };
        window.addEventListener('message', processMsg);
        this.initComplete = true;
    }

    sendMsg(type, payload) {
        if (this.videoWindow) {
            this.videoWindow.postMessage({ type, payload }, '*');
        }
    }

    connect(accessToken) {
        if (this.options.onConnect) {
            this.options.onConnect();
        }

        if (this.videoWindow) {
            console.log('sending message to video conference to connect');
            this.videoWindow.postMessage({ type: 'INITIAL_LOAD', payload: accessToken }, '*');
        } else {
            console.log('Not able to connect');
        }
    }

    hangUp() {
        if (this.videoWindow) {
            this.videoWindow.postMessage({ type: 'HANG_UP' }, '*');
        } else {
            console.log('Not able to hangup');
        }

        if (this.options.onHangUp)
            this.options.onHangUp();
    }
}