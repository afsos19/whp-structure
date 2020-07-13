export const unmaskCPF = cpf => cpf.replace(/\D/g, '');

export const maskPhone = phone =>
    `(${phone.substring(0, 2)}) ${phone.substring(2, 7)}-${phone.substring(7, 11)}`;
