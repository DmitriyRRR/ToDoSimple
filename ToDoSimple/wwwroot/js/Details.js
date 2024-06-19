// this isworking too
/*$(document).ready(function () {
    $("#details").click(function () {
        alert('a');
    })
})*/
// this isworking too
//$(document).on("click", "#details",
//    function ()
//    {
//        alert('a');
//    });

// sample
$(document).on("click", "#details", async function (e) {
    alert('a');

    let itemId = $(this).data("id");
    let value = $(this).val();

    var url = "/Home/Details";

    await fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }

    })
        .catch(function (error) {
            console.error(error);
        });
    location.reload()
    console.log(data)
});

$(document).on("click", "#some", function () {
    location.reload();
    alert('not a    ');
});