import {CLICK} from '../enums/index';

const initialState = { count: 0 };
const clickReducer = (state = initialState, action) => {
  switch (action.type) {
    case CLICK.Click:
      return {
        ...state,
        count: state.count + 1,
      };
    default:
      return { ...state };
  }
};
export default clickReducer;
