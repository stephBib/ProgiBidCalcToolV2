<template>
  <h1>Progi Bid Calculation Tool</h1>&nbsp;
  <div class="container">
    <div class="row">
      <!-- Vehicle Price -->
      <div class="col-3">{{ vehiclePriceLabel }}</div>
      <div class="col-3">
        <CurrencyInput class="form-control" id='vehiclePrice' v-model.lazy="vehiclePrice" :options="{ currency: 'USD' }" />
      </div>
      <!-- Vehicle Type -->
      <div class="col-3">{{ vehicleTypeLabel }}</div>
      <div class="col-3">
        <select id="vehicleType" placeholder="Select a Vehicle Type" v-model="selectedVehicleType" class="form-select">
          <option :value="vehicleType.id" v-for="vehicleType in vehicleTypedata" :key="vehicleType.id">
            {{ vehicleType.type }}
          </option>
        </select>
      </div>
    </div>

  </div>
  <br />
  <!-- Results -->
  <div class="container" style="margin-top: 50px; width: 900px;">
    <table border="1" class="table table-bordered">
      <thead>
        <tr>
          <th rowspan="2">Vehicle Price ($)</th>
          <th rowspan="2">Vehicle Type</th>
          <th colspan="4">Fees ($)</th>
          <th rowspan="2">Total ($)</th>
        </tr>
        <tr>
          <th>Basic</th>
          <th>Special</th>
          <th>Association</th>
          <th>Storage</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td class="bold right">
            {{ this.rsltVehiclePrice }}
          </td>
          <td style="text-align: center;">
            {{ this.rsltVehicleType }}
          </td>
          <td class="right">
            {{ this.rsltBasicFee }}
          </td>
          <td class="right">
            {{ this.rsltSpecialFee }}
          </td>
          <td class="right">
            {{ this.rsltAssociationFee }}
          </td>
          <td class="right">
            {{ this.rsltStorageFee }}
          </td>
          <td class="bold right">
            {{ this.rsltTotalFee }}
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
  import CurrencyInput from "./components/CurrencyInput.vue";

  const API_URL = "http://localhost:5230/"; // HARD CODED... check for global setting

  export default {
    name: "App",
    components: {
      CurrencyInput
    },
    data() {
      return {
        vehiclePriceLabel: 'Vehicle Price',
        vehicleTypeLabel: 'Vehicle Type',
        vehiclePrice: 0,
        selectedVehicleType: null,
        vehicleTypedata: [
          {
            id: 1, type: "Common"
          },
          {
            id: 2, type: "Luxury"
          }
        ],
        loading: false,
        errored: false,
        rsltVehiclePrice: null,
        rsltVehicleType: null,
        rsltBasicFee: null,
        rsltSpecialFee: null,
        rsltAssociationFee: null,
        rsltStorageFee: null,
        rsltTotalFee: null
      }
    },
    watch: {
      // trap state changes
      vehiclePrice: function (val) {
        this.vehiclePrice = val
        this.calculateFees()
      },
      selectedVehicleType: function (val) {
        this.selectedVehicleType = val
        this.calculateFees()
      }
    },
    methods: {
      // call server
      async calculateFees() {
        if (this.vehiclePrice > 0 && this.selectedVehicleType > 0) {
          axios.get(API_URL + "api/ProgiBidCalculator/CalculateFees", {
            params: {
              vehiclePrice: this.vehiclePrice,
              vehicleTypeId: this.selectedVehicleType
            },
          }).then(
            (response) => {
              this.rsltVehiclePrice = JSON.parse(response.data.VehiclePrice).toFixed(2)
              this.rsltVehicleType = response.data.VehicleType
              this.rsltBasicFee = JSON.parse(response.data.BasicFee).toFixed(2)
              this.rsltSpecialFee = JSON.parse(response.data.SpecialFee).toFixed(2)
              this.rsltAssociationFee = JSON.parse(response.data.AssociationFee).toFixed(2)
              this.rsltStorageFee = JSON.parse(response.data.StorageFee).toFixed(2)
              this.rsltTotalFee = JSON.parse(response.data.TotalPrice).toFixed(2)

              console.log(JSON.parse('{"foo": 1.00}'))
            }
          ).catch(error => console.log(error))
        }
      }
    },
    computed: {
      formattedJSON() {
        return JSON.stringify(this.results, null, 2);
      }
    }
  };
</script>

<style>
  .center-box {
    left: 20%;
    margin-top: 15%;
  }

  .label {
    display: block;
    text-align: center;
  }

  .inputs {
    display: block;
    background-color: #0094FE;
    color: white !important;
    width: 15%;
    height: 8%;
    min-width: 100px;
    margin: auto;
  }

  .myDiv {
    border: 5px outset red;
    background-color: lightblue;
    text-align: center;
  }

  th {
    text-align: center;
    font-weight: bold;
  }

  .bold {
    font-weight: bold;
  }

  .right {
    text-align: right;
  }
</style>
