

    function showUploadWidget() {
        cloudinary.openUploadWidget({
                cloudName: "namnguyen159198",
                uploadPreset: "ml_default",
                sources: [
                    "local",
                    "url",
                    "camera",
                    "image_search",
                    "facebook",
                    "dropbox",
                    "instagram",
                    "shutterstock"
                ],
                form: ".product-form",
                fieldName: "thumbnails[]",
                thumbnails: ".uploaded",
                multiple: true,
            },
            (err, info) => {
                if (!err) {
                    console.log("Upload Widget event - ", info);
                }
            });
    }
$('body').on('click', '.cloudinary-delete', function () {
    var spiltedLink = $(this).prev().attr('src').split('/');
    var imgId = spiltedLink[spiltedLink.length - 1].split('.')[0];
    $(`input[data-cloudinary-public-id="${imgId}"]`).remove();
});
document.getElementById("upload_widget").addEventListener("click", function () {
    showUploadWidget();
}, false);
