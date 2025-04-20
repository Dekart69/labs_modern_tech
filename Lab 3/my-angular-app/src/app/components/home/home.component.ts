import { Component, OnDestroy, OnInit } from '@angular/core';
import { HttpService } from '../../services/http-service';
import { Post } from '../../models/post';
import { Subject, takeUntil } from 'rxjs';
import { Router } from '@angular/router';
import { WindowService } from '../../services/window-service';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss',
  standalone: true,
})
export class HomeComponent implements OnInit, OnDestroy {
  posts?: Post[];
  isAuthorized: boolean = false;
  private readonly localStorage: Storage;

  private unsubscribeAdd$ = new Subject<void>();

  constructor(
    private readonly httpService: HttpService,
    private router: Router,
    private readonly window: WindowService,
  ) {
    this.localStorage = this.window.nativeWindow.localStorage;
  }

  ngOnInit(): void {
    const hasToken = this.localStorage.getItem("token");

    if (hasToken) {
      this.isAuthorized = true;
    }

    this.httpService.getPosts()
      .pipe(takeUntil(this.unsubscribeAdd$))
      .subscribe((p) => {
        this.posts = p;
      });
  }

  ngOnDestroy(): void {
    this.unsubscribeAdd$.next();
    this.unsubscribeAdd$.complete();
  }

  redirectAuth() {
    this.router.navigate(['/auth']);
  }

  writePost() {
    this.router.navigate(['/post']);
  }
}
