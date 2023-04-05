export function beforeStart(options, extensions) {
}

export function afterStarted(blazor) {
    blazor.registerCustomEventType('modalkeydown', {
        createEventArgs: (event) => {
            return {
                key: event.detail
            };
        }
    });
}
