const urlBase = 'https://programamaistop.com.br/api/';
const urlBaseNoApi = 'https://programamaistop.com.br/';

export const imgUserPath = `${urlBase}/wwwroot/Content/PhotoPerfil/`;
export const imgNewsPath = `${urlBase}/wwwroot/Content/News/`;
export const imgCampignPath = `${urlBase}/wwwroot/Content/Campaign/`;
export const imgProductsPath = `${urlBase}/wwwroot/Content/Products/`;
export const imgOccurrencePath = `${urlBase}/wwwroot/Content/Occurrence/`;
export const pdfRegulation = network => `${urlBaseNoApi}_assets/img/regulamento/Regulamento-Programa-maistop_${network.replace(/\s/g, '')}.pdf`;
export const pdfFirstAccess = network => `${urlBaseNoApi}_assets/img/regulamento/Regulamento-Programa-maistop_${network.replace(/\s/g, '')}.pdf`;
const baseNetworkPath = 'https://programamaistop.com.br/_assets/img/logos-redes';

export const imgNetworkPath = network => {
    return `${baseNetworkPath}/rede${network}.jpg`
};