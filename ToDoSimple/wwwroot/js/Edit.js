const notes = document.querySelectorAll('.note');

notes.forEach(note => {
    let dltBtn = note.querySelector('.delete');
    let editBtn = note.querySelector('.edit');
    let url = '/api/delete';

    dltBtn.addEventListener('click', async () => {
        const dataId = note.querySelector('.deleteId').textContent;
        if (confirm("Are you shure delete " + dataId + "?")) {

            await fetch(url, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(dataId)
            })
                .then(response => {
                    if (!response.ok) {
                        alert(response.status + " error");
                        throw new Error('Network response was not ok');
                    }
                    else {
                        alert("Note deleted successfully");
                    }
                    return response.json();
                })
                .catch(error => {
                    console.error("Fetch error:", error);
                });
            location.reload();
        }
        else {
            location.reload();
            alert("reload");
        }
    });
});