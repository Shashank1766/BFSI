$(function () {
    $("#BidDetailsFormDiv").validationEngine();
});

function SaveBidDetails(id) {
    var isValid = jQuery("#BidDetailsFormDiv").validationEngine({ returnIsValid: true });
    if (isValid == true) {
             var txtId = $("#txtId").val();
             var txtCategoryforCUG = $("#txtCategoryforCUG").val();
             var txtIncaseofMix = $("#txtIncaseofMix").val();
             var txtLocation = $("#txtLocation").val();
             var txtSellerName = $("#txtSellerName").val();
             var txtLoanAccountNumber = $("#txtLoanAccountNumber").val();
             var txtRegisteredOwner = $("#txtRegisteredOwner").val();
             var txtVehicleRegistrationNumber = $("#txtVehicleRegistrationNumber").val();
             var txtYearofManufacture = $("#txtYearofManufacture").val();
             var txtMake = $("#txtMake").val();
             var txtModel = $("#txtModel").val();
             var txtVariant = $("#txtVariant").val();
             var txtColour = $("#txtColour").val();
             var txtMileagekms = $("#txtMileagekms").val();
             var txtRCStatus = $("#txtRCStatus").val();
             var txtInsuranceStatus = $("#txtInsuranceStatus").val();
             var txtInsuranceValidTill = $("#txtInsuranceValidTill").val();
             var txtFuelType = $("#txtFuelType").val();
             var txtTax = $("#txtTax").val();
             var txtFitness = $("#txtFitness").val();
             var txtPermit = $("#txtPermit").val();
             var txtEngineNo = $("#txtEngineNo").val();
             var txtChasisNo = $("#txtChasisNo").val();
             var txtVehicleType = $("#txtVehicleType").val();
             var txtNumberofOwners = $("#txtNumberofOwners").val();
             var txtYardName = $("#txtYardName").val();
             var txtParkingaddress = $("#txtParkingaddress").val();
             var txtParkingCharges = $("#txtParkingCharges").val();
             var txtSpecialClause1 = $("#txtSpecialClause1").val();
             var txtSpecialClause2 = $("#txtSpecialClause2").val();
             var txtSpecialClause3 = $("#txtSpecialClause3").val();
             var txtSpecialClause4 = $("#txtSpecialClause4").val();
             var txtSpecialClause5 = $("#txtSpecialClause5").val();
             var txtStartingPrice = $("#txtStartingPrice").val();
             var txtReservePrice = $("#txtReservePrice").val();
             var txtBidIncrement = $("#txtBidIncrement").val();
             var txtBundleName = $("#txtBundleName").val();
             var txtStartDateTime = $("#txtStartDateTime").val();
             var txtEndDateTime = $("#txtEndDateTime").val();
             var txtExtensionTime = $("#txtExtensionTime").val();
             var txtEventType = $("#txtEventType").val();
             var txtBidCount = $("#txtBidCount").val();
             var txtShowStartPrice = $("#txtShowStartPrice").val();
             var txtShowCurrentPrice = $("#txtShowCurrentPrice").val();
             var txtSellerContactPerson = $("#txtSellerContactPerson").val();
             var txtSellerContactNumber = $("#txtSellerContactNumber").val();
             var txtBidkaroContactPerson = $("#txtBidkaroContactPerson").val();
             var txtBidkaroContactNumber = $("#txtBidkaroContactNumber").val();
             var txtSellerCode = $("#txtSellerCode").val();
             var txtBuyerCommissionRate = $("#txtBuyerCommissionRate").val();
             var txtSMSBidding = $("#txtSMSBidding").val();
             var txtImageURL = $("#txtImageURL").val();
             var txtEnablePG = $("#txtEnablePG").val();
        var jsonData = JSON.stringify({
             Id: txtId
             CategoryforCUG: txtCategoryforCUG
             IncaseofMix: txtIncaseofMix
             Location: txtLocation
             SellerName: txtSellerName
             LoanAccountNumber: txtLoanAccountNumber
             RegisteredOwner: txtRegisteredOwner
             VehicleRegistrationNumber: txtVehicleRegistrationNumber
             YearofManufacture: txtYearofManufacture
             Make: txtMake
             Model: txtModel
             Variant: txtVariant
             Colour: txtColour
             Mileagekms: txtMileagekms
             RCStatus: txtRCStatus
             InsuranceStatus: txtInsuranceStatus
             InsuranceValidTill: txtInsuranceValidTill
             FuelType: txtFuelType
             Tax: txtTax
             Fitness: txtFitness
             Permit: txtPermit
             EngineNo: txtEngineNo
             ChasisNo: txtChasisNo
             VehicleType: txtVehicleType
             NumberofOwners: txtNumberofOwners
             YardName: txtYardName
             Parkingaddress: txtParkingaddress
             ParkingCharges: txtParkingCharges
             SpecialClause1: txtSpecialClause1
             SpecialClause2: txtSpecialClause2
             SpecialClause3: txtSpecialClause3
             SpecialClause4: txtSpecialClause4
             SpecialClause5: txtSpecialClause5
             StartingPrice: txtStartingPrice
             ReservePrice: txtReservePrice
             BidIncrement: txtBidIncrement
             BundleName: txtBundleName
             StartDateTime: txtStartDateTime
             EndDateTime: txtEndDateTime
             ExtensionTime: txtExtensionTime
             EventType: txtEventType
             BidCount: txtBidCount
             ShowStartPrice: txtShowStartPrice
             ShowCurrentPrice: txtShowCurrentPrice
             SellerContactPerson: txtSellerContactPerson
             SellerContactNumber: txtSellerContactNumber
             BidkaroContactPerson: txtBidkaroContactPerson
             BidkaroContactNumber: txtBidkaroContactNumber
             SellerCode: txtSellerCode
             BuyerCommissionRate: txtBuyerCommissionRate
             SMSBidding: txtSMSBidding
             ImageURL: txtImageURL
             EnablePG: txtEnablePG
            //BidDetailsId: id,
            //BidDetailsMainPhone: txtBidDetailsMainPhone,
            //BidDetailsFax: txtBidDetailsFax,
            //BidDetailsLicenseNumberExpire: dtBidDetailsLicenseNumberExpire,
            // 2MAPCOLUMNSHERE - BidDetails
        });
        $.ajax({
            type: "POST",
            url: '/BidDetails/SaveBidDetails',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: jsonData,
            success: function (data) {
                ClearAll();
                var msg = "Records Saved successfully !";
                if (id > 0)
                    msg = "Records updated successfully";

                ShowMessage(msg, "Success", "success", true);
            },
            error: function (msg) {

            }
        });
    }
}

function EditBidDetails(id) {
    var jsonData = JSON.stringify({
             Id: txtId
             CategoryforCUG: txtCategoryforCUG
             IncaseofMix: txtIncaseofMix
             Location: txtLocation
             SellerName: txtSellerName
             LoanAccountNumber: txtLoanAccountNumber
             RegisteredOwner: txtRegisteredOwner
             VehicleRegistrationNumber: txtVehicleRegistrationNumber
             YearofManufacture: txtYearofManufacture
             Make: txtMake
             Model: txtModel
             Variant: txtVariant
             Colour: txtColour
             Mileagekms: txtMileagekms
             RCStatus: txtRCStatus
             InsuranceStatus: txtInsuranceStatus
             InsuranceValidTill: txtInsuranceValidTill
             FuelType: txtFuelType
             Tax: txtTax
             Fitness: txtFitness
             Permit: txtPermit
             EngineNo: txtEngineNo
             ChasisNo: txtChasisNo
             VehicleType: txtVehicleType
             NumberofOwners: txtNumberofOwners
             YardName: txtYardName
             Parkingaddress: txtParkingaddress
             ParkingCharges: txtParkingCharges
             SpecialClause1: txtSpecialClause1
             SpecialClause2: txtSpecialClause2
             SpecialClause3: txtSpecialClause3
             SpecialClause4: txtSpecialClause4
             SpecialClause5: txtSpecialClause5
             StartingPrice: txtStartingPrice
             ReservePrice: txtReservePrice
             BidIncrement: txtBidIncrement
             BundleName: txtBundleName
             StartDateTime: txtStartDateTime
             EndDateTime: txtEndDateTime
             ExtensionTime: txtExtensionTime
             EventType: txtEventType
             BidCount: txtBidCount
             ShowStartPrice: txtShowStartPrice
             ShowCurrentPrice: txtShowCurrentPrice
             SellerContactPerson: txtSellerContactPerson
             SellerContactNumber: txtSellerContactNumber
             BidkaroContactPerson: txtBidkaroContactPerson
             BidkaroContactNumber: txtBidkaroContactNumber
             SellerCode: txtSellerCode
             BuyerCommissionRate: txtBuyerCommissionRate
             SMSBidding: txtSMSBidding
             ImageURL: txtImageURL
             EnablePG: txtEnablePG
        Id: id
    });
    $.ajax({
        type: "POST",
        url: '/BidDetails/GetBidDetails',
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: jsonData,
        success: function (data) {
            $('#BidDetailsFormDiv').empty();
            $('#BidDetailsFormDiv').html(data);
            $('#collapseBidDetailsAddEdit').addClass('in');
            $("#BidDetailsFormDiv").validationEngine();
        },
        error: function (msg) {

        }
    });
}

function DeleteBidDetails(id) {
    if (confirm("Do you want to delete this record? ")) {
        var txtBidDetailsId = id;
        var jsonData = JSON.stringify({
             Id: txtId
             CategoryforCUG: txtCategoryforCUG
             IncaseofMix: txtIncaseofMix
             Location: txtLocation
             SellerName: txtSellerName
             LoanAccountNumber: txtLoanAccountNumber
             RegisteredOwner: txtRegisteredOwner
             VehicleRegistrationNumber: txtVehicleRegistrationNumber
             YearofManufacture: txtYearofManufacture
             Make: txtMake
             Model: txtModel
             Variant: txtVariant
             Colour: txtColour
             Mileagekms: txtMileagekms
             RCStatus: txtRCStatus
             InsuranceStatus: txtInsuranceStatus
             InsuranceValidTill: txtInsuranceValidTill
             FuelType: txtFuelType
             Tax: txtTax
             Fitness: txtFitness
             Permit: txtPermit
             EngineNo: txtEngineNo
             ChasisNo: txtChasisNo
             VehicleType: txtVehicleType
             NumberofOwners: txtNumberofOwners
             YardName: txtYardName
             Parkingaddress: txtParkingaddress
             ParkingCharges: txtParkingCharges
             SpecialClause1: txtSpecialClause1
             SpecialClause2: txtSpecialClause2
             SpecialClause3: txtSpecialClause3
             SpecialClause4: txtSpecialClause4
             SpecialClause5: txtSpecialClause5
             StartingPrice: txtStartingPrice
             ReservePrice: txtReservePrice
             BidIncrement: txtBidIncrement
             BundleName: txtBundleName
             StartDateTime: txtStartDateTime
             EndDateTime: txtEndDateTime
             ExtensionTime: txtExtensionTime
             EventType: txtEventType
             BidCount: txtBidCount
             ShowStartPrice: txtShowStartPrice
             ShowCurrentPrice: txtShowCurrentPrice
             SellerContactPerson: txtSellerContactPerson
             SellerContactNumber: txtSellerContactNumber
             BidkaroContactPerson: txtBidkaroContactPerson
             BidkaroContactNumber: txtBidkaroContactNumber
             SellerCode: txtSellerCode
             BuyerCommissionRate: txtBuyerCommissionRate
             SMSBidding: txtSMSBidding
             ImageURL: txtImageURL
             EnablePG: txtEnablePG
            Id: id
        });
        $.ajax({
            type: "POST",
            url: '/BidDetails/DeleteBidDetails',
            async: false,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            data: jsonData,
            success: function (data) {
                if (data) {
                    BindBidDetailsGrid();
                    ShowMessage("Records Deleted Successfully", "Sucess", "success", true);
                }
                else {
                    return false;
                }
            },
            error: function (msg) {
                return true;
            }
        });
    }
}

function BindBidDetailsGrid() {
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "/BidDetails/BindBidDetailsList",
        dataType: "html",
        async: true,
        data: null,
        success: function (data) {
            $("#BidDetailsListDiv").empty();
            $("#BidDetailsListDiv").html(data);
        },
        error: function (msg) {

        }

    });
}

function ClearForm() {
    
}

function ClearAll() {
    $("#BidDetailsFormDiv").clearForm();
    $('#collapseBidDetailsAddEdit').removeClass('in');
    $('#collapseBidDetailsList').addClass('in');
    $("#BidDetailsFormDiv").validationEngine();
    $.validationEngine.closePrompt(".formError", true);
    $.ajax({
        type: "POST",
        url: '/BidDetails/ResetBidDetailsForm',
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        data: null,
        success: function (data) {
            if (data) {
                $('#BidDetailsFormDiv').empty();
                $('#BidDetailsFormDiv').html(data);
                $('#collapseBidDetailsList').addClass('in');
                BindBidDetailsGrid();
            }
            else {
                return false;
            }
        },
        error: function (msg) {


            return true;
        }
    });

}




