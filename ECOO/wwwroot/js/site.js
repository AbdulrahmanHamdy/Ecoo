/**
 * ECOO — site.js
 * Lightweight vanilla-JS helpers. No jQuery dependency.
 */

// Auto-dismiss Bootstrap alerts after 5 seconds
document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.alert.alert-success, .alert.alert-warning').forEach(el => {
        setTimeout(() => {
            const bsAlert = bootstrap.Alert.getOrCreateInstance(el);
            bsAlert?.close();
        }, 5000);
    });
});