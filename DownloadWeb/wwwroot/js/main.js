function fallbackCopyTextToClipboard(text) {
    var textArea = document.createElement("textarea");
    textArea.value = text;

    // Avoid scrolling to bottom
    textArea.style.top = "0";
    textArea.style.left = "0";
    textArea.style.position = "fixed";

    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Fallback: Copying text command was ' + msg);
    } catch (err) {
        console.error('Fallback: Oops, unable to copy', err);
    }

    document.body.removeChild(textArea);
}

function showMsgNearElement(element, msg, duration) {
    var el = document.createElement("div");
    el.setAttribute("style", "background-color:white;");
    el.innerHTML = msg;
    setTimeout(function () {
        el.parentNode.removeChild(el);
    }, duration);
    element.parentElement.appendChild(el);
}

window.copyUrlToClipBoard = (e, url) => {
    if (!navigator.clipboard) {
        fallbackCopyTextToClipboard(url);
        return;
    }
    navigator.clipboard.writeText(url).then(function () {
        console.log('Async: Copying to clipboard was successful!');
        showMsgNearElement(e, "Copied", 1500);
    }, function (err) {
        console.error('Async: Could not copy text: ', err);
    });
}

window.downloadVideoFromUrl = (url) => {
    fetch(url).then(res => res.blob()).then(file => {
        let tempUrl = URL.createObjectURL(file);
        const aTag = document.createElement("a");
        aTag.href = tempUrl;
        aTag.download = url.replace(/^.*[\\\/]/, '');
        document.body.appendChild(aTag);
        aTag.click();
        downloadBtn.innerText = "Download File";
        URL.revokeObjectURL(tempUrl);
        aTag.remove();
    }).catch(() => {
        alert("Failed to download file!");
        downloadBtn.innerText = "Download File";
    });
}