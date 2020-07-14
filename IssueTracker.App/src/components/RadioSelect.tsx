import React from 'react';

// export const Select = <T extends string | number | undefined>({
//     onChange,
//     checkValue,
//     inputRef,
//     value,
//     ...props
//   }: RadioProps<T>) => {
//     return (
//       <input
//         {...withoutAdditionalProps(props)}
//         checked={checkValue === value}
//         type="radio"
//         ref={inputRef}
//         onBlur={() => onBlur()}
//         onChange={() => onChange(checkValue)}
//         value={value ?? ''}
//       />
//     );
//   };