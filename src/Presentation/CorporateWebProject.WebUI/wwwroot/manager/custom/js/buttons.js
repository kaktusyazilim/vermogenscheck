function btnPassive(itemid, itemurl) {
    Swal.fire({
        title: "Emin misiniz?",
        text: "Bu veriyi pasif hale getirmek istediğinize emin misiniz? Getirdikten sonra sadece siz görüntüleyebilirsiniz.",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#16C7F9",
        cancelButtonColor: "#FC4438",
        cancelButtonText: "Hayır, vazgeçtim",
        confirmButtonText: "Evet, Pasif Et!",
    }).then((result) => {
        if (result.isConfirmed) {
            Freeze(FreezeUI, UnFreezeUI);
            $.ajax({
                url: itemurl,
                data: { id: itemid },
                success: function (result) {
                    UnFreeze(FreezeUI, UnFreezeUI);
                    Swal.fire({
                        title: "İşlem Tamamlandı!",
                        text: "Veri başarıyla pasif edildi.",
                        icon: "success",
                    });
                },
                error: function (result) {
                    Swal.fire({
                        title: "İşlem Başarısız!",
                        text: "İşlem yapılırken bir hata meydana geldi. Hata: " + result,
                        icon: "error",
                    });
                }
            })
           
        }
    });

}

function btnDelete(itemid, itemurl) {
    Swal.fire({
        title: "Emin misiniz?",
        text: "Bu veriyi silmek istediğinize emin misiniz? Bir daha geri alamazsınız",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#16C7F9",
        cancelButtonColor: "#FC4438",
        cancelButtonText: "Hayır, vazgeçtim",
        confirmButtonText: "Evet, Sil!",
    }).then((result) => {
        if (result.isConfirmed) {
            Freeze(FreezeUI, UnFreezeUI);
            $.ajax({
                url: itemurl,
                data: { id: itemid },
                success: function (result) {
                    UnFreeze(FreezeUI, UnFreezeUI);
                    Swal.fire({
                        title: "İşlem Tamamlandı!",
                        text: "Veri başarıyla silindi.  ",
                        icon: "success",
                    }).then((result) => {

                        location.reload();
                    }
                    )
                },
                error: function (result) {
                    Swal.fire({
                        title: "İşlem Başarısız!",
                        text: "İşlem yapılırken bir hata meydana geldi. Hata: " + result,
                        icon: "error",
                    });
                }
            })

        }
    });

}


function Freeze(FreezeFun, UnFreezeFun) {
    let currentTimeout = null; // Variable to store the current timeout
    let currentFreezeFunction = null; // Variable to store the current freeze function
    let currentUnfreezeFunction = null; // Variable to store the current unfreeze function
    // Clear the current timeout and stop the ongoing functionality
    clearTimeout(currentTimeout);
    if (currentFreezeFunction && currentUnfreezeFunction) {
        currentUnfreezeFunction();
    }

    // Set the new freeze and unfreeze functions
    currentFreezeFunction = FreezeFun;
    currentUnfreezeFunction = UnFreezeFun;

    // Call the freeze function
    FreezeFun();

   
}
function UnFreeze(FreezeFun, UnFreezeFun) {
    let currentTimeout = null; // Variable to store the current timeout
    let currentFreezeFunction = null; // Variable to store the current freeze function
    let currentUnfreezeFunction = null; // Variable to store the current unfreeze function
    // Clear the current timeout and stop the ongoing functionality
    clearTimeout(currentTimeout);
    if (currentFreezeFunction && currentUnfreezeFunction) {
        currentUnfreezeFunction();
    }

    // Set the new freeze and unfreeze functions
    currentFreezeFunction = FreezeFun;
    currentUnfreezeFunction = UnFreezeFun;

    // Call the freeze function
    FreezeFun();

    // Set a new timeout to call the unfreeze function after 1 second
    currentTimeout = setTimeout(() => {
        UnFreezeFun();
        currentFreezeFunction = null; // Reset current freeze function after unfreezing
        currentUnfreezeFunction = null; // Reset current unfreeze function after unfreezing
    }, 100);
}


$(document).ready(function () {
    $(".True .action .passive i").css("color", "white")
    $(".True .action .passive i").css("font-size", "13px")
    $(".True .action .passive i").css("background-color", "green")
    $(".True .action .passive i").css("padding", "5px")
    $(".True .action .passive i").css("border-radius", "12px")
    $(".True .action .passive i").removeClass();
    $(".True .action .passive i").addClass("fa fa-check");
    $(".True .action .passive i").css("opacity","1")
})