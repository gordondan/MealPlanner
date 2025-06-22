console.log('Loading modal-helper.js');
console.log('Bootstrap available at load:', typeof window.bootstrap);

window.modalHelper = {
    show: function (modalId) {
        console.log('modalHelper.show called with modalId:', modalId);
        try {
            // Check if bootstrap is available
            if (!window.bootstrap) {
                console.error('Bootstrap is not available on window object');
                return false;
            }
            console.log('Bootstrap is available:', typeof window.bootstrap);

            // Check if modal element exists
            const modalElement = document.getElementById(modalId);
            if (!modalElement) {
                console.error('Modal element not found:', modalId);
                return false;
            }
            console.log('Modal element found:', modalElement);

            // Create and show modal
            console.log('Creating Bootstrap modal instance');
            const modal = new bootstrap.Modal(modalElement);
            console.log('Modal instance created:', modal);
            
            console.log('Calling modal.show()');
            modal.show();
            console.log('Modal.show() completed successfully');
            return true;
        } catch (error) {
            console.error('Error showing modal:', error);
            console.error('Error stack:', error.stack);
        }
        return false;
    },
    
    hide: function (modalId) {
        console.log('modalHelper.hide called with modalId:', modalId);
        try {
            // Check if bootstrap is available
            if (!window.bootstrap) {
                console.error('Bootstrap is not available on window object');
                return false;
            }

            // Check if modal element exists
            const modalElement = document.getElementById(modalId);
            if (!modalElement) {
                console.error('Modal element not found:', modalId);
                return false;
            }
            console.log('Modal element found:', modalElement);

            // Get existing modal instance and hide it
            console.log('Getting Bootstrap modal instance');
            const modal = bootstrap.Modal.getInstance(modalElement);
            if (!modal) {
                console.error('No modal instance found for element:', modalId);
                return false;
            }
            console.log('Modal instance found:', modal);
            
            console.log('Calling modal.hide()');
            modal.hide();
            console.log('Modal.hide() completed successfully');
            return true;
        } catch (error) {
            console.error('Error hiding modal:', error);
            console.error('Error stack:', error.stack);
        }
        return false;
    }
};