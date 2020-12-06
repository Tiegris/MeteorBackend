mdata = []

function FetchAndLog() {
    console.log('piros')
    fetch('/api/schemes')
        .then(response => response.json())
        .then(data => console.log(data))
        .then(data => setData(data));
    console.log('körte')
}

function setData(x) {
    mdata = x;
}

