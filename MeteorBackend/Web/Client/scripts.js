mdata = []

class SchemeData {
    constructor(name, ver, s1, s2, s3, s4, s5, s6) {
        this.Name = name;
        this.Version = ver;
        this.Swarm1 = s1;
        this.Swarm2 = s2;
        this.Swarm3 = s3;
        this.Swarm4 = s4;
        this.Swarm5 = s5;
        this.Swarm6 = s6;
    }
}

function fetchList() {
    fetch('/api/schemes?limit=8')
        .then(response => response.json())
        .then(data => setData(data)).catch((error) => {
            var list = document.getElementById("loading_sign");
            list.innerHTML = "Can't reach database";
        });
}

function setData(x) {
    mdata = x;
    renderList();
}

function renderList() {
    var list = document.getElementById("list");
    list.innerHTML = "";

    mdata.forEach(scheme => {
        var head = document.createElement("div");
        head.className = "d-flex";
        var head_name = document.createElement("div");
        head_name.innerHTML = scheme.name;
        var head_ver = document.createElement("div");
        head_ver.innerHTML = "v" + scheme.version;
        head_ver.className = "ml-auto"
        head.appendChild(head_name);
        head.appendChild(head_ver);

        var date = document.createElement("div");
        date.innerHTML = "Last updated: " + scheme.date_time;

        var swarmListHeader = document.createElement("div");
        swarmListHeader.innerHTML = "Meteor showers:"

        var swarmList = document.createElement("div");
        swarmList.className = "d-flex align-content-start flex-wrap";        

        if (!!scheme.swarm1) {
            var s = document.createElement("div");
            s.className = "mr-1";
            s.innerHTML = scheme.swarm1 + ",";
            swarmList.appendChild(s);
        }
        if (!!scheme.swarm2) {
            var s = document.createElement("div");
            s.className = "mr-1";
            s.innerHTML = scheme.swarm2 + ",";
            swarmList.appendChild(s);
        }
        if (!!scheme.swarm3) {
            var s = document.createElement("div");
            s.className = "mr-1";
            s.innerHTML = scheme.swarm3 + ",";
            swarmList.appendChild(s);
        }
        if (!!scheme.swarm4) {
            var s = document.createElement("div");
            s.className = "mr-1";
            s.innerHTML = scheme.swarm4 + ",";
            swarmList.appendChild(s);
        }
        if (!!scheme.swarm5) {
            var s = document.createElement("div");
            s.className = "mr-1";
            s.innerHTML = scheme.swarm5 + ",";
            swarmList.appendChild(s);
        }
        if (!!scheme.swarm6) {
            var s = document.createElement("div");
            s.className = "mr-1";
            s.innerHTML = scheme.swarm6 + ",";
            swarmList.appendChild(s);
        }
        var s = document.createElement("div");
        s.innerHTML = "SPO";
        swarmList.appendChild(s);

        var buttons = document.createElement("div");
        buttons.className = "d-flex";
        var buttons_aligner = document.createElement("div");
        buttons_aligner.className = "btn-group ml-auto";

        var btnEdit = document.createElement("button");
        btnEdit.className = "btn btn-outline-secondary fas fa-pencil-alt"
        btnEdit.onclick = function () {
            var id = mdata.indexOf(scheme);
            openToEdit(id);
        }
        var btnDelete = document.createElement("button");
        btnDelete.className = "btn btn-outline-danger fas fa-trash"
        btnDelete.onclick = function() {            
            deleteScheme(scheme.id);
        }
        buttons_aligner.appendChild(btnEdit);
        buttons_aligner.appendChild(btnDelete);

        buttons.appendChild(buttons_aligner);

        var row = document.createElement("div");
        row.className = "list-group-item col";
        row.appendChild(head);
        row.appendChild(date);
        row.appendChild(swarmListHeader);
        row.appendChild(swarmList);
        row.appendChild(buttons);

        list.appendChild(row)
    });


}


function sendForm() {
    form = document.getElementById("editForm")
    let sd = new SchemeData(
        form.querySelector('input[id="inputName"]').value,
        parseInt(form.querySelector('input[id="inputVer"]').value),
        form.querySelector('input[id="inputSwarm1"]').value,
        form.querySelector('input[id="inputSwarm2"]').value,
        form.querySelector('input[id="inputSwarm3"]').value,
        form.querySelector('input[id="inputSwarm4"]').value,
        form.querySelector('input[id="inputSwarm5"]').value,
        form.querySelector('input[id="inputSwarm6"]').value
    );

    jsonData = JSON.stringify(sd);

    $.ajax({
        url: '/api/schemes',
        type: 'POST',
        data: jsonData,
        dataType: 'json',
        processData: false,
        'contentType': 'application/json',
        success: function () {
            fetchList()

            document.getElementById("inputName").value = null;
            document.getElementById("inputVer").value = null;
            document.getElementById("inputSwarm1").value = null;
            document.getElementById("inputSwarm2").value = null;
            document.getElementById("inputSwarm3").value = null;
            document.getElementById("inputSwarm4").value = null;
            document.getElementById("inputSwarm5").value = null;
            document.getElementById("inputSwarm6").value = null;
        }
    });

}

function deleteScheme(id) {
    $.ajax({
        url: '/api/schemes/'+id,
        type: 'DELETE',
        success: function () {
            fetchList()
        }
    });
}

function openToEdit(id) {
    var scheme = mdata[id];
    document.getElementById("inputName").value = scheme.name ? scheme.name : null;
    document.getElementById("inputVer").value = scheme.version ? scheme.version : null;

    document.getElementById("inputSwarm1").value = scheme.swarm1 ? scheme.swarm1 : null;
    document.getElementById("inputSwarm2").value = scheme.swarm2 ? scheme.swarm2 : null;
    document.getElementById("inputSwarm3").value = scheme.swarm3 ? scheme.swarm3 : null;
    document.getElementById("inputSwarm4").value = scheme.swarm4 ? scheme.swarm4 : null;
    document.getElementById("inputSwarm5").value = scheme.swarm5 ? scheme.swarm5 : null;
    document.getElementById("inputSwarm6").value = scheme.swarm6 ? scheme.swarm6 : null;
}

fetchList();