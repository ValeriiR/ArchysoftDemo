import { Component } from '@angular/core';
import { TranslationService } from 'src/app/core/services/translation.service';


import { locale as enLang } from '../assets/i18n/en';
import { locale as ruLang } from '../assets/i18n/ru';
import { locale as uaLang } from '../assets/i18n/ua';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  constructor(private translationService: TranslationService) {
    translationService.loadTranslations(enLang, ruLang, uaLang);
    translationService.setLanguage('en');
  }

}
