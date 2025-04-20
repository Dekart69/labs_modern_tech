import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';  // Імпортуємо FormsModule для template-driven форм
import { AppComponent } from './app.component';  // Ваш головний компонент
import { PostComponent } from './components/post/post.component';  // Ваш компонент

@NgModule({
    declarations: [
        AppComponent,
        PostComponent
    ],
    imports: [
        BrowserModule,
        FormsModule  // Додаємо FormsModule для роботи з ngModel
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
