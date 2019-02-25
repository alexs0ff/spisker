/* dev */
import * as moment from 'moment';
/* end-dev */

/* prod**    
import moment from 'moment';
**end-prod */

export class RuLocale {
    
    private static readonly localeRu: string = "ru-ru";

    private static readonly monthsParse: any = [/^янв/i, /^фев/i, /^мар/i, /^апр/i, /^ма[йя]/i, /^июн/i, /^июл/i, /^авг/i, /^сен/i, /^окт/i, /^ноя/i, /^дек/i];

    private static  plural(word:any, num:any):any {
        var forms = word.split('_');
        return num % 10 === 1 && num % 100 !== 11 ? forms[0] : (num % 10 >= 2 && num % 10 <= 4 && (num % 100 < 10 || num % 100 >= 20) ? forms[1] : forms[2]);
    }
    private static relativeTimeWithPlural(number:any, withoutSuffix:any, key:any):any {
        var format = {
            'mm': withoutSuffix ? 'минута_минуты_минут' : 'минуту_минуты_минут',
            'hh': 'час_часа_часов',
            'dd': 'день_дня_дней',
            'MM': 'месяц_месяца_месяцев',
            'yy': 'год_года_лет'
        };
        if (key === 'm') {
            return withoutSuffix ? 'минута' : 'минуту';
        }
        else {
            return number + ' ' + RuLocale.plural(format[key], +number);
        }
    }

    static setLocale() {
        moment.locale(RuLocale.localeRu);
    }

    static defineLocale() {

        moment.defineLocale(RuLocale.localeRu,
            {
                months: {
                    format: 'января_февраля_марта_апреля_мая_июня_июля_августа_сентября_октября_ноября_декабря'.split('_'),
                    standalone: 'январь_февраль_март_апрель_май_июнь_июль_август_сентябрь_октябрь_ноябрь_декабрь'.split('_')
                },
                weekdays: {
                    standalone: 'воскресенье_понедельник_вторник_среда_четверг_пятница_суббота'.split('_'),
                    format: 'воскресенье_понедельник_вторник_среду_четверг_пятницу_субботу'.split('_'),
                    isFormat: /\[ ?[Вв] ?(?:прошлую|следующую|эту)? ?\] ?dddd/
                },
                weekdaysShort: 'вс_пн_вт_ср_чт_пт_сб'.split('_'),
                weekdaysMin: 'вс_пн_вт_ср_чт_пт_сб'.split('_'),
                monthsParse: RuLocale.monthsParse,
                longMonthsParse: RuLocale.monthsParse,
                shortMonthsParse: RuLocale.monthsParse,
                // полные названия с падежами, по три буквы, для некоторых, по 4 буквы, сокращения с точкой и без точки
                monthsRegex: /^(январ[ья]|янв\.?|феврал[ья]|февр?\.?|марта?|мар\.?|апрел[ья]|апр\.?|ма[йя]|июн[ья]|июн\.?|июл[ья]|июл\.?|августа?|авг\.?|сентябр[ья]|сент?\.?|октябр[ья]|окт\.?|ноябр[ья]|нояб?\.?|декабр[ья]|дек\.?)/i,

                // копия предыдущего
                monthsShortRegex: /^(январ[ья]|янв\.?|феврал[ья]|февр?\.?|марта?|мар\.?|апрел[ья]|апр\.?|ма[йя]|июн[ья]|июн\.?|июл[ья]|июл\.?|августа?|авг\.?|сентябр[ья]|сент?\.?|октябр[ья]|окт\.?|ноябр[ья]|нояб?\.?|декабр[ья]|дек\.?)/i,

                // полные названия с падежами
                monthsStrictRegex: /^(январ[яь]|феврал[яь]|марта?|апрел[яь]|ма[яй]|июн[яь]|июл[яь]|августа?|сентябр[яь]|октябр[яь]|ноябр[яь]|декабр[яь])/i,
                monthsShortStrictRegex: /^(янв\.|февр?\.|мар[т.]|апр\.|ма[яй]|июн[ья.]|июл[ья.]|авг\.|сент?\.|окт\.|нояб?\.|дек\.)/i,
                longDateFormat: {
                    LT: 'HH:mm',
                    LTS: 'HH:mm:ss',
                    L: 'DD.MM.YYYY',
                    LL: 'D MMMM YYYY г.',
                    LLL: 'D MMMM YYYY г., HH:mm',
                    LLLL: 'dddd, D MMMM YYYY г., HH:mm'
                },
                calendar: {
                    sameDay: '[Сегодня в] LT',
                    nextDay: '[Завтра в] LT',
                    lastDay: '[Вчера в] LT',
                    nextWeek: function (now:any) {
                        if (now.week() !== this.week()) {
                            switch (this.day()) {
                            case 0:
                                return '[В следующее] dddd [в] LT';
                            case 1:
                            case 2:
                            case 4:
                                return '[В следующий] dddd [в] LT';
                            case 3:
                            case 5:
                            case 6:
                                return '[В следующую] dddd [в] LT';
                            }
                        } else {
                            if (this.day() === 2) {
                                return '[Во] dddd [в] LT';
                            } else {
                                return '[В] dddd [в] LT';
                            }
                        }
                    },
                    lastWeek: function (now:any) {
                        if (now.week() !== this.week()) {
                            switch (this.day()) {
                            case 0:
                                return '[В прошлое] dddd [в] LT';
                            case 1:
                            case 2:
                            case 4:
                                return '[В прошлый] dddd [в] LT';
                            case 3:
                            case 5:
                            case 6:
                                return '[В прошлую] dddd [в] LT';
                            }
                        } else {
                            if (this.day() === 2) {
                                return '[Во] dddd [в] LT';
                            } else {
                                return '[В] dddd [в] LT';
                            }
                        }
                    },
                    sameElse: 'L'
                },
                relativeTime: {
                    future: 'через %s',
                    past: '%s назад',
                    s: 'несколько секунд',
                    m: RuLocale.relativeTimeWithPlural,
                    mm: RuLocale.relativeTimeWithPlural,
                    h: 'час',
                    hh: RuLocale.relativeTimeWithPlural,
                    d: 'день',
                    dd: RuLocale.relativeTimeWithPlural,
                    M: 'месяц',
                    MM: RuLocale.relativeTimeWithPlural,
                    y: 'год',
                    yy: RuLocale.relativeTimeWithPlural
                },

                meridiemParse: /ночи|утра|дня|вечера/i,
                isPM: input => /^(дня|вечера)$/.test(input),
                meridiem: (hour:any, minute:any, isLower:any) => {
                    if (hour < 4) {
                        return 'ночи';
                    } else if (hour < 12) {
                        return 'утра';
                    } else if (hour < 17) {
                        return 'дня';
                    } else {
                        return 'вечера';
                    }
                },
                ordinalParse: /\d{1,2}-(й|го|я)/,
                ordinal: (number:number):string => number + '-й',
                week: {
                    dow: 1, // Monday is the first day of the week.
                    doy: 7  // The week that contains Jan 1st is the first week of the year.
                }
            });
    }
}