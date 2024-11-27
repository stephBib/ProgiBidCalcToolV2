import axios from 'axios';

const instance = axios.create({
    baseURL: 'api/'
});

// const API_URL = "http://localhost:5139/";
// 
//,
//async deleteNotes(id) {
//    axios.delete(API_URL + "api/BidCalculator/DeleteNotes?id=" + id).then(
//        (response) => {
//            this.refreshData();
//            alert(response.data);
//        }
//    )
//}
export default {
  install: function(Vue) {
    Object.defineProperty(Vue.prototype, '$axios', { value: instance });
  }
}
