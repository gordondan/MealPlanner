window.modalHelper = {
    show: function (modalId) {
        try {
            const modalElement = document.getElementById(modalId);
            if (modalElement && window.bootstrap) {
                const modal = new bootstrap.Modal(modalElement);
                modal.show();
                return true;
            }
        } catch (error) {
            console.error('Error showing modal:', error);
        }
        return false;
    },
    
    hide: function (modalId) {
        try {
            const modalElement = document.getElementById(modalId);
            if (modalElement && window.bootstrap) {
                const modal = bootstrap.Modal.getInstance(modalElement);
                if (modal) {
                    modal.hide();
                    return true;
                }
            }
        } catch (error) {
            console.error('Error hiding modal:', error);
        }
        return false;
    }
};