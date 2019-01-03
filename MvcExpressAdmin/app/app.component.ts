import { Component } from '@angular/core';
@Component({
    selector: 'app',
    template: '<h1>Page Says: {{text}}</h1>'
})
export class AppComponent { text = 'Hello Angular 2'; }  