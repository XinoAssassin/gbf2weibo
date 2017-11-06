function install_notice() {
    if (localStorage.getItem('install_time'))   
        return;

    var now = new Date().getTime();
    localStorage.setItem('install_time', now);
    chrome.tabs.create({url: "guide.html"});
}

install_notice();