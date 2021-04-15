import {Iinformation} from "./Iinformation.js"

const ADDRES='http://127.0.0.1:8080/';

class Main{
    url:string
    
    constructor(url:string){
        this.url = url
        var btnSearch: any = document.getElementById("btn-search")
        var btnReload: any = document.getElementById("btn-reload")
        var txtSearch: any = document.getElementById("txt-search")
        btnSearch.onclick = (e: Event) => {
            this.search(txtSearch?.value)
        }
        btnReload.onclick=(e:Event)=>{
           this.get().then((data)=>{
               this.show(data)
           })
        }
    }
    get(): Promise<any> {
        return fetch(this.url)
            .then(response => response.json())     
    }
    delete(id: number): any {
        fetch(`${ADDRES}delete?id=${id}`).then((value) => {
            value.json().then((data) => {
                if (data.status) {
                    document.getElementById(`row${id}`)?.remove()
                }
            })
        })
    }
    clearTable():void{
        var table: any = document.getElementById("tb_main");
        //clear old item
        table.innerHTML=null;
        var row = table.insertRow(0)
        row.insertCell(0).innerHTML = "#"
        row.insertCell(1).innerHTML = "First"
        row.insertCell(2).innerHTML = "Last"
        row.insertCell(3).innerHTML = "Address"
        row.insertCell(4).innerHTML = "Email"
        row.insertCell(5).innerHTML = "Time"
        row.insertCell(6).innerHTML = "option"
    }
    show(data: Array<Iinformation>): void{
        var rowCount: number = 1
        var table: any = document.getElementById("tb_main");
        //clear old item
        this.clearTable()
        data.forEach((element:Iinformation) => {
            var row = table.insertRow(rowCount)
            row.setAttribute('id', `row${element.Id}`)
            row.insertCell(0).innerHTML = rowCount;
            row.insertCell(1).innerHTML = element.Firstname;
            row.insertCell(2).innerHTML = element.Lastname;
            row.insertCell(3).innerHTML = element.Address;
            row.insertCell(4).innerHTML = element.Email;
            row.insertCell(5).innerHTML = element.time;
            var btn:HTMLElement = document.createElement('button');
            btn.textContent = 'Delete';
            btn.setAttribute('class','btn btn-danger')
            btn.onclick = (e) => {
                this.delete(element.Id)
            }
            row.insertCell(6).appendChild(btn);
            rowCount++
        });
    }
    search(name: string): void {
        if(name.length>1){
            fetch(`${this.url}find?name=${name}`)
               .then(response =>response.json().then(data => this.show(data)))
        }
    }
}

var main = new Main(ADDRES);
main.get().then((value)=>{
    main.show(value)
})
