function showInputForm(roomId) {
    var RoomNameInput = document.getElementById('eRoomName-' + roomId);
    var CapacityInput = document.getElementById('eRoomCapacity-' + roomId);
    var OkeyButton = document.getElementById('eOkeyButton-' + roomId);

    RoomNameInput.style.display = 'inline';
    CapacityInput.style.display = 'inline';
    OkeyButton.style.display = 'inline';
}