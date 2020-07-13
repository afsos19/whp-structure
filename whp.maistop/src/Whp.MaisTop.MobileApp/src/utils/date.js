/* eslint-disable import/prefer-default-export */
import 'moment';
import moment from 'moment-timezone';
import 'moment/locale/pt-br';

export const returnMonth = monthNumber => {
    switch (monthNumber) {
        case 1:
            return 'Janeiro';
        case 2:
            return 'Fevereiro';
        case 3:
            return 'Março';
        case 4:
            return 'Abril';
        case 5:
            return 'Maio';
        case 6:
            return 'Junho';
        case 7:
            return 'Julho';
        case 8:
            return 'Agosto';
        case 9:
            return 'Setembro';
        case 10:
            return 'Outubro';
        case 11:
            return 'Novembro';
        case 12:
            return 'Dezembro';
        default:
            return 'Janeiro';
    }
};

export const allMonths = () => {
    return [
        {
            label: 'Janeiro',
            value: 1,
        },
        {
            label: 'Fevereiro',
            value: 2,
        },
        {
            label: 'Março',
            value: 3,
        },
        {
            label: 'Abril',
            value: 4,
        },
        {
            label: 'Maio',
            value: 5,
        },
        {
            label: 'Junho',
            value: 6,
        },
        {
            label: 'Julho',
            value: 7,
        },
        {
            label: 'Agosto',
            value: 8,
        },
        {
            label: 'Setembro',
            value: 9,
        },
        {
            label: 'Outubro',
            value: 10,
        },
        {
            label: 'Novembro',
            value: 11,
        },
        {
            label: 'Dezembro',
            value: 12,
        },
    ];
};

export const allYears = () => {
    const years = [];
    const currentYear = moment().format('YYYY');

    for (let index = parseInt(currentYear); index >= 2017; index--) {
        years.push({
            label: index.toString(),
            value: index,
        });
    }

    return years;
};
