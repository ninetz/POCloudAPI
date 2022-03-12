import { Component, OnInit } from '@angular/core';
import { delay } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor() { }

  ngOnInit(): void {
    const typedTextSpan = document.querySelector(".typed-text");
    const cursorSpan = document.querySelector(".cursor");

    const textArray = ["Hi! I'm a back-end software developer studying Information and Network Technologies. I am most excited to work in Android/.Net development environments. Head over to projects to see what else I've done in my spare time :)"];
    const typingDelay = 60;
    const newTextDelay = 50; // Delay between current and next text
    let textArrayIndex = 0;
    let charIndex = 0;

    function type() {
      if (charIndex < textArray[textArrayIndex].length) {
        if (!cursorSpan.classList.contains("typing")) cursorSpan.classList.add("typing");
        typedTextSpan.textContent += textArray[textArrayIndex].charAt(charIndex);
        charIndex++;
        setTimeout(type, typingDelay);
      }
    }

    

    document.addEventListener("DOMContentLoaded", function () { // On DOM Load initiate the effect
      if (textArray.length) setTimeout(type, newTextDelay + 250);
    });
  }


}

