import { Component, OnDestroy } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpService } from '../../services/http-service';
import { Subject } from 'rxjs';
import { WindowService } from '../../services/window-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-auth',
  imports: [FormsModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss',
  standalone: true,
})
export class AuthComponent implements OnDestroy {
  private readonly localStorage: Storage;

  private unsubscribeAdd$ = new Subject<void>();

  constructor(
    private readonly httpService: HttpService,
    private readonly window: WindowService,
    private router: Router
  ) {
    this.localStorage = this.window.nativeWindow.localStorage;
  }

  ngOnDestroy(): void {
    this.unsubscribeAdd$.next();
    this.unsubscribeAdd$.complete();
  }

  onSubmit(form: any) {
    this.httpService.createUser(form.value.name, form.value.password)
      .pipe()
      .subscribe(
        token => {
          this.localStorage.setItem("token", token);
          this.router.navigate(['/home']);
        },
        error => {
          console.error('Error creating user:', error);
        }
      );
  }
}
