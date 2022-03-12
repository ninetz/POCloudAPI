import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-my-certifications',
  templateUrl: './my-certifications.component.html',
  styleUrls: ['./my-certifications.component.css']
})
export class MyCertificationsComponent implements OnInit {

  constructor() { }
  previousImg: number = 1;
  ngOnInit(): void {

  }
  showImg(id) {

    var obj = document.getElementById("picture" + this.previousImg)
    if (obj != null)  obj.className = 'hide'
    document.getElementById("picture" + id).className = 'show';

    this.previousImg = id;
  }
}
