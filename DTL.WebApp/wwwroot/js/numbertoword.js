//==Author : Virendra V
//==Date : 23-12-2022
//==Dependency : Need Common Jquery library 

function NumberToWord() {
    let self = this
    this.ToWord = function (number) {
        let amount = number.split('.');
        let toWord = '';
        switch (amount[0].length) {            
            case 2: toWord = this.getTens(amount[0]); break;
            case 3: toWord = this.getHunderds(amount[0]); break;
            case 4: toWord = this.getThousands(amount[0]); break;
            case 5: toWord = this.getTenthThousands(amount[0]); break;
            case 6: toWord = this.getLakhs(amount[0]); break;
            case 7: toWord = this.getTenthLakhs(amount[0]); break;
            default: ''; break;
        }
        return toWord.replace(/\s+/g, ' ');
    }

    this.getDigit = function (digit) {
        switch (digit) {
            case '01': return 'One'; break;
            case '1': return 'One'; break;
            case '02': return 'Two'; break;
            case '2': return 'Two'; break;
            case '03': return 'Three'; break;
            case '3': return 'Three'; break;
            case '04': return 'Four'; break;
            case '4': return 'Four'; break;
            case '05': return 'Five'; break;
            case '5': return 'Five'; break;
            case '06': return 'Six'; break;
            case '6': return 'Six'; break;
            case '07': return 'Seven'; break;
            case '7': return 'Seven'; break;
            case '8': return 'Eight'; break;
            case '08': return 'Eight'; break;
            case '09': return 'Nine'; break;
            case '9': return 'Nine'; break;
            case '10': return 'Ten'; break;
            case '11': return 'Eleven'; break;
            case '12': return 'Twelve'; break;
            case '13': return 'Thirteen'; break;
            case '14': return 'Fourteen'; break;
            case '15': return 'Fifteen'; break;
            case '16': return 'Sixteen'; break;
            case '17': return 'Seventeen'; break;
            case '18': return 'Eighteen'; break;
            case '19': return 'Nineteen'; break;
            case '20': return 'Twenty'; break;
            case '30': return 'Thirty'; break;
            case '40': return 'Fourty'; break;
            case '50': return 'Fifty'; break;
            case '60': return 'Sixty'; break;
            case '70': return 'Seventy'; break;
            case '80': return 'Eighty'; break;
            case '90': return 'Ninty'; break;
            case '100': return 'Hundred'; break;
            default: return ''; break;
        };
    };

    this.getTenthLakhs = function (val) {
        let word = '';
        if (val[0] !== '1') {
            word = this.getDigit(val[0] + '0') + ' lakh ' + (val[2] !== '0' ? this.getTenthThousands(val.substring(2)).toLowerCase() : this.getThousands(val.substring(3)).toLowerCase());
        }
        else {
            word = this.getDigit(val[0] + '' + val[1]) + ' lakh ' + (val[2] !== '0' ? this.getTenthThousands(val.substring(2)).toLowerCase() : this.getThousands(val.substring(3)).toLowerCase());
        }
        return word;
    };

    this.getLakhs = function (val) {
        let word = '';
        if (val[0] !== '1') {
            word = this.getDigit(val[0]) + ' lakh ' + (val[1] !== '0' ? this.getTenthThousands(val.substring(1)).toLowerCase() : this.getThousands(val.substring(2)).toLowerCase());
        }
        else {
            word = this.getDigit(val[0]) + ' lakh ' + (val[1] !== '0' ? this.getTenthThousands(val.substring(1)).toLowerCase() : this.getThousands(val.substring(2)).toLowerCase());
        }
        return word;
    };

    this.getTenthThousands = function (val) {
        let word = '';
        if (val[0] !== '1') {
            word = this.getDigit(val[0] + '0') + ' ' + this.getDigit(val[1]).toLowerCase() + ' thousand ' + this.getHunderds(val.substring(2)).toLowerCase();
        }
        else {
            word = this.getDigit(val[0] + '' + val[1]) + ' thousand ' + this.getHunderds(val.substring(2)).toLowerCase();
        }
        return word;
    };

    this.getThousands = function (val) {
        let word = '';
        word = this.getDigit(val[0]) + ' thousand ' + this.getHunderds(val.substring(1)).toLowerCase();
        return word;
    };

    this.getHunderds = function (val) {
        let word = '';
        word = (val[0] !== '0' ? (this.getDigit(val[0]) + ' hundred ') : '') + this.getTens(val.substring(1)).toLowerCase();
        return word;
    };

    this.getTens = function (val) {
        let word = '';
        if (!this.getDigit(val)) {
            word = ' and ' + this.getDigit(val[0] + '0') + '' + this.getDigit(val[1]).toLocaleLowerCase() + ' rupees only /-';
        }
        else {
            word = ' and ' + this.getDigit(val) + '' + ' rupees only';
        }
        return ((val[0] === '0' && val[1] === '0') ? word.replace('and', '') : word);
    };
}

